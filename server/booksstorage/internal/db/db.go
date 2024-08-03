package db

import (
	"booksstorage/internal/models"
	"context"
	"log/slog"

	"go.mongodb.org/mongo-driver/bson"
	"go.mongodb.org/mongo-driver/mongo"
	"go.mongodb.org/mongo-driver/mongo/options"
)

type MongoDatabase struct {
	Client *mongo.Client
	Coll   *mongo.Collection
}

func ConnectToDB(dbCred models.DbCred, ctx context.Context) (*MongoDatabase, error) {
	clientOptions := options.Client().ApplyURI(dbCred.Uri).SetAuth(dbCred.Credentials)
	client, err := mongo.Connect(ctx, clientOptions)

	if err != nil {
		return nil, err
	}

	if err := client.Ping(ctx, nil); err != nil {
		return nil, err
	}

	coll := client.Database(dbCred.Database).Collection(dbCred.Collection)

	return &MongoDatabase{
		Client: client,
		Coll:   coll,
	}, nil
}

func (m *MongoDatabase) DisconnectFromDB(ctx context.Context) {
	if err := m.Client.Disconnect(ctx); err != nil {
		panic(err)
	}
}

func (m *MongoDatabase) GetAllBooks(ctx context.Context) ([]models.Book, error) {
	var results []models.Book

	cur, err := m.Coll.Find(ctx, bson.D{})

	err = cur.All(ctx, &results)

	if err == mongo.ErrNoDocuments {
		slog.Warn("No books were found",
			"error", err)
		return nil, err
	}

	for _, book := range results {
		slog.Info("Book founded.",
			"bookName", book.Name)
	}

	slog.Info("Results length",
		"length", len(results))

	return results, nil
}

func (m *MongoDatabase) GetBooksByName(name string, ctx context.Context) ([]models.Book, error) {
	var results []models.Book

	slog.Info("Finding books with the following name:",
		"bookName", name)

	cur, err := m.Coll.Find(ctx, bson.D{{Key: "name", Value: name}})

	err = cur.All(ctx, &results)

	if err == mongo.ErrNoDocuments {
		slog.Warn("No books were found",
			"error", err)
		return nil, err
	}

	for _, book := range results {
		slog.Info("Book founded.",
			"bookName", book.Name)
	}

	return results, nil
}

func (m *MongoDatabase) InsertOneBook(book models.Book, ctx context.Context) error {
	result, err := m.Coll.InsertOne(ctx, book)

	if err != nil {
		slog.Error("An error ocurred while trying to insert one document",
			"error", err)
		return err
	}

	slog.Info("Book sucessfully added to db.",
		"bsonId", result.InsertedID)

	return nil
}

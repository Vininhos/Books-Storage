package db

import (
	"booksstorage/internal/models"
	"context"
	"fmt"
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

	coll := client.Database(dbCred.Uri).Collection(dbCred.Collection)

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

	if err = cur.All(ctx, &results); err != nil {
		panic(err)
	}

	if err == mongo.ErrNoDocuments {
		fmt.Printf("No books were found\n")
		return nil, err
	}

	for _, book := range results {
		fmt.Printf("Book founded: %s\n", book.Name)
	}

	fmt.Printf("results length: %d\n", len(results))

	return results, nil
}

func (m *MongoDatabase) GetBooksByName(name string, ctx context.Context) ([]models.Book, error) {
	var results []models.Book

	slog.Info("Finding books with the following name:", slog.String("Book name", name))
	cur, err := m.Coll.Find(ctx, bson.D{{Key: "name", Value: name}})

	if err = cur.All(ctx, &results); err != nil {
		panic(err)
	}

	if err == mongo.ErrNoDocuments {
		fmt.Printf("No document was found with the title \"%s\"\n", name)
		return nil, err
	}

	for _, book := range results {
		fmt.Printf("Book founded: %s", book.Name)
	}

	return results, nil
}

func (m *MongoDatabase) InsertOneBook(book models.Book, ctx context.Context) error {
	result, err := m.Coll.InsertOne(ctx, book)

	if err != nil {
		fmt.Printf("An error occuried while trying to insert one document. Err: %s\n", err.Error())
		return err
	}

	fmt.Printf("Book was inserted with the following id: %s\n", result.InsertedID)

	return nil
}

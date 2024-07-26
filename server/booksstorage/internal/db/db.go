package db

import (
	"booksstorage/internal/models"
	"context"
	"errors"
	"fmt"
	"os"

	"go.mongodb.org/mongo-driver/bson"
	"go.mongodb.org/mongo-driver/mongo"
	"go.mongodb.org/mongo-driver/mongo/options"
)

var (
	client *mongo.Client
	coll   *mongo.Collection
)

func ConnectToDB(ctx context.Context) error {
	if client != nil {
		return errors.New("already connected to db")
	}

	uri := os.Getenv("MONGODB_URI")

	if uri == "" {
		return errors.New("URI environment could not be loaded. Please check the environment keys on your .env or the K8s env manifests")
	}

	credential := options.Credential{
		Username: os.Getenv("MONGODB_USERNAME"),
		Password: os.Getenv("MONGODB_PASSWORD"),
	}

	if credential.Username == "" || credential.Password == "" {
		return errors.New("URI environment could not be loaded. Please check the environment keys on your .env or the K8s env manifests")
	}

	client, err := mongo.Connect(ctx, options.Client().ApplyURI(uri).SetAuth(credential))

	if err != nil {
		return err
	}

	coll = client.Database(os.Getenv("MONGODB_DATABASE")).Collection(os.Getenv("MONGODB_COLLECTION"))

	return nil
}

func DisconnectFromDB(ctx context.Context) {
	if err := client.Disconnect(ctx); err != nil {
		panic(err)
	}
}

func GetAllBooks(ctx context.Context) ([]models.Book, error) {
	var results []models.Book

	cur, err := coll.Find(ctx, bson.D{})

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

func GetBooksByName(name string, coll *mongo.Collection, ctx context.Context) ([]models.Book, error) {
	var results []models.Book

	cur, err := coll.Find(ctx, bson.D{{"Name", name}})

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

func InsertOneBook(book models.Book, coll *mongo.Collection, ctx context.Context) error {
	result, err := coll.InsertOne(ctx, book)

	if err != nil {
		fmt.Printf("An error occuried while trying to insert one document. Err: %s\n", err.Error())
		return err
	}

	fmt.Printf("Book was inserted with the following id: %s\n", result.InsertedID)

	return nil
}

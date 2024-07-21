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
	err    error
)

func ConnectToDB() error {
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

	client, err = mongo.Connect(context.TODO(), options.Client().ApplyURI(uri).SetAuth(credential))

	if err != nil {
		return err
	}

	coll = client.Database(os.Getenv("MONGODB_DATABASE")).Collection(os.Getenv("MONGODB_COLLECTION"))

	return nil
}

func DisconnectFromDB() error {
	if client == nil {
		return errors.New("the application is not connected to db")
	}
	if err := client.Disconnect(context.TODO()); err != nil {
		panic(err)
	}

	fmt.Println("Disconnected from db")

	return nil
}

func GetAllBooks() ([]models.Book, error) {
	var results []models.Book

	cur, err := coll.Find(context.TODO(), bson.D{})

	if err = cur.All(context.TODO(), &results); err != nil {
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

func GetBooksByName(name string) ([]models.Book, error) {
	var results []models.Book

	cur, err := coll.Find(context.TODO(), bson.D{{"Name", name}})

	if err = cur.All(context.TODO(), &results); err != nil {
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

func InsertOneBook(book models.Book) error {
	result, err := coll.InsertOne(context.TODO(), book)

	if err != nil {
		fmt.Printf("An error occuried while trying to insert one document. Err: %s\n", err.Error())
		return err
	}

	fmt.Printf("Book was inserted with the following id: %s\n", result.InsertedID)

	return nil
}

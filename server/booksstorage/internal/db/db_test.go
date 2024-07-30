package db_test

import (
	"booksstorage/internal/db"
	"booksstorage/internal/models"
	"context"
	"fmt"
	"os"
	"testing"

	"github.com/stretchr/testify/assert"
	"go.mongodb.org/mongo-driver/bson"
	"go.mongodb.org/mongo-driver/mongo/integration/mtest"
)

func TestGetAllBooks(t *testing.T) {
	tdb := "booksstorage_test"
	coll := "books_test"

	err := os.Setenv("MONGODB_DATABASE", tdb)
	if err != nil {
		t.Errorf("error while setting the MONGODB_DATABASE environment variable: %s.", err)
		t.FailNow()
	}
	defer os.Clearenv()

	mtestdb := mtest.New(t, mtest.NewOptions().ClientType(mtest.Mock))

	mtestdb.Run("when_sending_a_request_returns_success", func(mt *mtest.T) {
		bookMock := []models.Book{
			{
				Name:     "Go Programming Language",
				Author:   "Alan A. A. Donovan",
				PubYear:  2015,
				Price:    240.0,
				Category: "Programming",
			},
			{
				Name:     "Clean Code",
				Author:   "Robert C. Martin",
				PubYear:  2008,
				Price:    120.0,
				Category: "Programming",
			},
			{
				Name:     "The Pragmatic Programmer",
				Author:   "Andrew Hunt",
				PubYear:  1999,
				Price:    150.0,
				Category: "Programming",
			},
		}

		bsonData, err := bson.Marshal(bookMock)
		if err != nil {
			t.Errorf("failed to marshal books to bson.D: %s", err)
			t.FailNow()
		}

		var bsonBooks []bson.D
		err = bson.Unmarshal(bsonData, &bsonBooks)

		cursorResponse := mtest.CreateCursorResponse(1, fmt.Sprintf("%s.%s", tdb, coll), mtest.FirstBatch, bsonBooks...)

		mt.AddMockResponses(cursorResponse)

		mt.Client.Database(tdb).Collection(coll)

		if err := db.ConnectToDB(context.TODO()); err != nil {
			t.Error(err)
		}

		books, err := db.GetAllBooks(context.TODO())
		if err != nil {
			t.Error(err)
		}

		assert.Len(t, books, len(bookMock))
	})
}

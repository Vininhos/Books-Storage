package api

import (
	"bytes"
	"encoding/json"
	"fmt"
	"github.com/brianvoe/gofakeit/v6"
	"io"
	"log/slog"
	"math/rand"
	"mongodb-feeder/pkg/logger"
	"mongodb-feeder/pkg/model"
	"net/http"
	"os"
)

func GetNewBook(i int) (model.BooksStorageModel, error) {
	logger.Logger.Info("Trying to get a new book from API.")
	res, err := http.Get(fmt.Sprintf(os.Getenv("ANOTHERBOOKAPIURL")+"%d", i))
	if err != nil {
		return model.BooksStorageModel{}, err
	}

	logger.Logger.Info("Reading message from response body.")
	body, err := io.ReadAll(res.Body)
	if err != nil {
		return model.BooksStorageModel{}, err
	}
	err = res.Body.Close()
	if err != nil {
		return model.BooksStorageModel{}, err
	}

	logger.Logger.Info("Response from API:", slog.String("response", string(body)))

	var book model.PublicApiBook

	logger.Logger.Info("Executing JSON Unmarshal from response.")
	err = json.Unmarshal(body, &book)
	if err != nil {
		return model.BooksStorageModel{}, err
	}

	logger.Logger.Info("Returning new book.")
	return model.BooksStorageModel{Name: book.Data.Title, Author: gofakeit.Name(), PublicationYear: book.Data.Year, Price: rand.Float64() * 3000, Category: "History", Email: gofakeit.Email()}, nil
}

func SendPayload(book model.BooksStorageModel) error {
	logger.Logger.Info("Executing JSON Marshal from book:", slog.String("name", book.Name))
	body, err := json.Marshal(book)
	if err != nil {
		return err
	}

	logger.Logger.Info("Generating payload.")
	payload := bytes.NewBuffer(body)

	logger.Logger.Info("Posting payload to the API.")
	res, err := http.Post("BOOKSSTORAGEAPIURL", "application/json", payload)
	if err != nil {
		return err
	}

	logger.Logger.Info("Response Status Code:", slog.Int("statusCode", res.StatusCode))

	return nil
}

package api

import (
	"bytes"
	"encoding/json"
	"fmt"
	"github.com/brianvoe/gofakeit/v6"
	"io"
	"log/slog"
	"math/rand"
	"mongodb-feeder/pkg/model"
	"net/http"
	"os"
)

var logger *slog.Logger

func init() {
	logger = slog.New(slog.NewJSONHandler(os.Stdout, nil))
}

func GetNewBook(i int) (model.BooksStorageModel, error) {
	logger.Info("Trying to get a new book from API.")
	res, err := http.Get(fmt.Sprintf("https://stephen-king-api.onrender.com/api/book/%d", i))
	if err != nil {
		return model.BooksStorageModel{}, err
	}

	logger.Info("Reading message from response body.")
	body, err := io.ReadAll(res.Body)
	if err != nil {
		return model.BooksStorageModel{}, err
	}
	err = res.Body.Close()
	if err != nil {
		return model.BooksStorageModel{}, err
	}

	logger.Info("Response from API:", slog.String("response", string(body)))

	var book model.PublicApiBook

	logger.Info("Executing JSON Unmarshal from response.")
	err = json.Unmarshal(body, &book)
	if err != nil {
		return model.BooksStorageModel{}, err
	}

	logger.Info("Returning new book.")
	return model.BooksStorageModel{Name: book.Data.Title, Author: gofakeit.Name(), PublicationYear: book.Data.Year, Price: rand.Float64() * 3000, Category: "History", Email: gofakeit.Email()}, nil
}

func SendPayload(book model.BooksStorageModel) error {
	logger.Info("Executing JSON Marshal from book:", slog.String("name", book.Name))
	body, err := json.Marshal(book)
	if err != nil {
		return err
	}

	logger.Info("Generating payload.")
	payload := bytes.NewBuffer(body)

	logger.Info("Posting payload to the API.")
	res, err := http.Post("http://localhost:8079/api/Book", "application/json", payload)
	if err != nil {
		return err
	}

	logger.Info("Response Status Code:", slog.Int("statusCode", res.StatusCode))

	return nil
}

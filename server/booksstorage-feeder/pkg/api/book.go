package api

import (
	"booksstorage-feeder/pkg/logger"
	"booksstorage-feeder/pkg/model"
	"bytes"
	"encoding/json"
	"github.com/brianvoe/gofakeit/v6"
	"log/slog"
	"net/http"
	"os"
)

func GetNewBook() model.BooksStorageModel {
	logger.Logger.Info("Generating new random book from gofakeit and returning it.")
	book := model.BooksStorageModel{Name: gofakeit.BookTitle(), Author: gofakeit.BookAuthor(), PublicationYear: gofakeit.Year(), Price: gofakeit.Price(99, 19999), Category: gofakeit.BookGenre(), Email: gofakeit.Email()}

	logger.Logger.Info("New book was generated.", slog.String("Book", book.Name))
	return book
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
	res, err := http.Post(os.Getenv("BOOKSSTORAGEAPIURL"), "application/json", payload)
	if err != nil {
		return err
	}

	logger.Logger.Info("Response Status Code:", slog.Int("statusCode", res.StatusCode))

	return nil
}

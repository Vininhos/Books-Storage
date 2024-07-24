package api

import (
	"booksstorage-feeder/pkg/logger"
	"booksstorage-feeder/pkg/model"
	"bytes"
	"encoding/json"
	"log/slog"
	"net/http"
	"os"

	"github.com/brianvoe/gofakeit/v6"
)

func GetNewBook() model.Book {
	logger.Logger.Info("Generating new random book from gofakeit and returning it.")
	book := model.Book{Name: gofakeit.BookTitle(), Author: gofakeit.BookAuthor(), PublicationYear: gofakeit.Year(), Price: gofakeit.Price(99, 19999), Category: gofakeit.BookGenre(), Email: gofakeit.Email()}

	logger.Logger.Info("New book was generated.", slog.String("Book", book.Name))
	return book
}

func SendPayload(book model.Book) error {
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

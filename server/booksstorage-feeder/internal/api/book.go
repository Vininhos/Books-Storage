package api

import (
	"booksstorage-feeder/internal/model"
	"booksstorage-feeder/pkg/log"
	"bytes"
	"encoding/json"
	"log/slog"
	"net/http"
	"os"

	"github.com/brianvoe/gofakeit/v6"
)

var logger *slog.Logger = log.GetLogger()

func GetNewBook() model.Book {
	logger.Info("Generating new random book from gofakeit and returning it.")
	book := model.Book{
		Name:            gofakeit.BookTitle(),
		Author:          gofakeit.BookAuthor(),
		PublicationYear: gofakeit.Year(),
		Price:           gofakeit.Price(99, 19999),
		Category:        gofakeit.BookGenre(),
		Email:           gofakeit.Email()}

	logger.Info("New book was generated.", slog.String("Book", book.Name))
	return book
}

func SendPayload(book model.Book) error {
	logger.Info("Executing JSON Marshal from book:", slog.String("name", book.Name))
	body, err := json.Marshal(book)
	if err != nil {
		return err
	}

	logger.Info("Generating payload.")
	payload := bytes.NewBuffer(body)

	logger.Info("Posting payload to the API.")
	res, err := http.Post(os.Getenv("BOOKSSTORAGEAPIURL"), "application/json", payload)
	if err != nil {
		return err
	}

	logger.Info("Response Status Code:", slog.Int("statusCode", res.StatusCode))

	return nil
}

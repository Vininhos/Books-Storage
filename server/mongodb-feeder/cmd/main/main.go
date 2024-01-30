package main

import (
	"log/slog"
	"mongodb-feeder/pkg/api"
	"mongodb-feeder/pkg/model"
	"time"
)

var book model.BooksStorageModel
var err error

func main() {
	for i := 1; i < 64; i++ {
		book, err = api.GetNewBook(i)
		if err != nil {
			slog.Error("Error while getting a new book:", slog.String("error", err.Error()))
		}

		err = api.SendPayload(book)
		if err != nil {
			slog.Error("Error while sending a new book to the API:", slog.String("error", err.Error()))
		}

		time.Sleep(time.Hour * 4)
	}
}

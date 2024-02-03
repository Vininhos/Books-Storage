package main

import (
	"booksstorage-feeder/pkg/api"
	"booksstorage-feeder/pkg/logger"
	"booksstorage-feeder/pkg/model"
	"log/slog"
	"math/rand"
	"time"
)

var book model.BooksStorageModel
var err error

func main() {
	for {
		book = api.GetNewBook()

		err = api.SendPayload(book)
		if err != nil {
			logger.Logger.Error("Error while sending a new book to the API:", slog.String("error", err.Error()))
		}

		r := rand.Intn(60000)

		time.Sleep(time.Minute * time.Duration(r))
	}
}

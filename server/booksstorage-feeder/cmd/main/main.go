package main

import (
	"booksstorage-feeder/internal/api"
	"booksstorage-feeder/internal/model"
	"log/slog"
	"math/rand"
	"os"
	"strconv"
	"time"
)

var (
	book model.Book
	err  error
	r    int = 60000
)

func init() {
	logger := slog.New(slog.NewJSONHandler(os.Stdout, nil))
	slog.SetDefault(logger)
}

func main() {
	rarg := os.Getenv("MAXMILLISEC")
	if rarg != "" {
		r, err = strconv.Atoi(rarg)
		if err != nil {
			panic(err)
		}
	}

	for {
		book = api.GetNewBook()

		err = api.SendPayload(book)
		if err != nil {
			slog.Error(
				"Error while sending a new book to the API:",
				"error", err)
		}

		r := rand.Intn(r)

		time.Sleep(time.Millisecond * time.Duration(r))
	}
}

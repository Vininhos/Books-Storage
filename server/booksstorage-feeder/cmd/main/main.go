package main

import (
	"booksstorage-feeder/internal/api"
	"booksstorage-feeder/internal/model"
	"booksstorage-feeder/pkg/log"
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

func main() {
	rarg := os.Getenv("MAXMILLISEC")
	if rarg != "" {
		r, err = strconv.Atoi(rarg)
		if err != nil {
			panic(err)
		}
	}
	logger := log.GetLogger()

	for {
		book = api.GetNewBook()

		err = api.SendPayload(book)
		if err != nil {
			logger.Error(
				"Error while sending a new book to the API:",
				slog.String("error", err.Error()),
			)
		}

		r := rand.Intn(r)

		time.Sleep(time.Millisecond * time.Duration(r))
	}
}

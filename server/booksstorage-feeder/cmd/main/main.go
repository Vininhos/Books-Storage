package main

import (
	"booksstorage-feeder/internal/api"
	"booksstorage-feeder/internal/logger"
	"booksstorage-feeder/internal/model"
	"fmt"
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
			panic(fmt.Sprintf("Error while converting environment variable to int. Error: %s", err))
		}
	}
	logger := logger.GetLogger()

	for {
		book = api.GetNewBook()

		err = api.SendPayload(book)
		if err != nil {
			logger.Logger.Error(
				"Error while sending a new book to the API:",
				slog.String("error", err.Error()),
			)
		}

		r := rand.Intn(r)

		time.Sleep(time.Millisecond * time.Duration(r))
	}
}

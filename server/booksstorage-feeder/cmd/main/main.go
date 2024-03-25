package main

import (
	"booksstorage-feeder/pkg/api"
	"booksstorage-feeder/pkg/logger"
	"booksstorage-feeder/pkg/model"
	"fmt"
	"log/slog"
	"math/rand"
	"os"
	"strconv"
	"time"
)

var book model.BooksStorageModel
var err error
var r int = 60000

func init() {
	rarg := os.Getenv("MAXMILLISEC")
	if rarg != "" {
		r, err = strconv.Atoi(rarg)
		if err != nil {
			panic(fmt.Sprintf("Error while converting environment variable to int. Error: %s", err))
		}
	}
}

func main() {
	for {
		book = api.GetNewBook()

		err = api.SendPayload(book)
		if err != nil {
			logger.Logger.Error("Error while sending a new book to the API:", slog.String("error", err.Error()))
		}

		r := rand.Intn(r)

		time.Sleep(time.Millisecond * time.Duration(r))
	}
}

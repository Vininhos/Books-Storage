package main

import (
	"booksstorage-email/internal/routes"
	"log"
	"log/slog"
	"net/http"
	"os"
	"time"

	"github.com/joho/godotenv"
)

const defaultTimeout = time.Minute

func init() {
	logger := slog.New(slog.NewJSONHandler(os.Stdout, nil))
	slog.SetDefault(logger)
}

func main() {
	if err := godotenv.Load(); err != nil {
		slog.Info("No .env file found")
	}

	r := routes.Routes()
	slog.Info("Listening to port 8080...")
	log.Fatal(http.ListenAndServe(":8080", r))
}

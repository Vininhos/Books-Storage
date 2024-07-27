package handlers

import (
	"booksstorage/internal/db"
	"booksstorage/internal/models"
	"encoding/json"
	"log/slog"
	"net/http"
)

func GetAllBooksHandler(w http.ResponseWriter, r *http.Request, logger *slog.Logger) {
	ctx := r.Context()
	book, err := db.GetAllBooks(ctx)
	if err != nil {
		logger.Error(err.Error())
	}

	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(book)
}

func InsertOneBookHandler(w http.ResponseWriter, r *http.Request, logger *slog.Logger) {
	ctx := r.Context()
	var book models.Book

	err := json.NewDecoder(r.Body).Decode(&book)
	if err != nil {
		logger.Error("An error happened when trying to decode the json payload", slog.String("Error:", err.Error()))
	}

	if err = db.InsertOneBook(book, ctx); err != nil {
		logger.Error("An error happened when trying to insert a book to db", slog.String("Error:", err.Error()))
	}

	w.WriteHeader(http.StatusCreated)
}

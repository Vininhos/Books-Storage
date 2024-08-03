package handlers

import (
	"booksstorage/internal/db"
	"booksstorage/internal/models"
	"encoding/json"
	"log/slog"
	"net/http"
)

func GetAllBooksHandler(w http.ResponseWriter, r *http.Request, mongoDatabase *db.MongoDatabase) {
	ctx := r.Context()
	book, err := mongoDatabase.GetAllBooks(ctx)
	if err != nil {
		slog.Error(err.Error())
	}

	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(book)
}

func InsertOneBookHandler(w http.ResponseWriter, r *http.Request, mongoDatabase *db.MongoDatabase) {
	ctx := r.Context()
	var book models.Book

	err := json.NewDecoder(r.Body).Decode(&book)
	if err != nil {
		slog.Error("An error happened when trying to decode the json payload", slog.String("Error:", err.Error()))
	}

	if err = mongoDatabase.InsertOneBook(book, ctx); err != nil {
		slog.Error("An error happened when trying to insert a book to db", slog.String("Error:", err.Error()))
	}

	w.WriteHeader(http.StatusCreated)
}

func GetBooksByNameHandler(w http.ResponseWriter, r *http.Request, name string, mongoDatabase *db.MongoDatabase) {
	ctx := r.Context()
	book, err := mongoDatabase.GetBooksByName(name, ctx)
	if err != nil {
		slog.Error(err.Error())
	}

	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(book)
}

package handlers

import (
	"booksstorage/internal/db"
	"booksstorage/internal/models"
	"encoding/json"
	"log/slog"
	"net/http"
)

// GetAllBooksHandler handle HTTP GET requests to access all books from database.
func GetAllBooksHandler(w http.ResponseWriter, r *http.Request, db *db.MongoDatabase) {
	ctx := r.Context()
	book, err := db.GetAllBooks(ctx)
	if err != nil {
		slog.Error(err.Error())
	}

	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(book)
}

// InsertOneBookAndSendEmailHandler handle HTTP POST requests to insert books and POST and request to mailer service.
func InsertOneBookHandler(w http.ResponseWriter, r *http.Request, db *db.MongoDatabase) {
	ctx := r.Context()
	var book models.Book

	err := json.NewDecoder(r.Body).Decode(&book)
	if err != nil {
		errorMsg := "An error happened when trying to decode the json payload"
		slog.Error(errorMsg, slog.String("Error:", err.Error()))
		http.Error(w, errorMsg, http.StatusBadRequest)
	}

	if err = db.InsertOneBook(book, ctx); err != nil {
		errorMsg := "An error happened when trying to insert the book to db"
		slog.Error(errorMsg, slog.String("Error:", err.Error()))
		http.Error(w, errorMsg, http.StatusBadRequest)
	}

	w.WriteHeader(http.StatusCreated)
}

// InsertOneBookAndSendEmailHandler handle HTTP POST requests to insert books and POST and request to mailer service.
func InsertOneBookAndSendEmailHandler(w http.ResponseWriter, r *http.Request, db *db.MongoDatabase) {
	ctx := r.Context()
	var book models.Book

	err := json.NewDecoder(r.Body).Decode(&book)
	if err != nil {
		errorMsg := "An error happened when trying to decode the json payload"
		slog.Error(errorMsg, slog.String("Error:", err.Error()))
		http.Error(w, errorMsg, http.StatusBadRequest)
	}

	if err = db.InsertOneBook(book, ctx); err != nil {
		errorMsg := "An error happened when trying to insert the book to db"
		slog.Error(errorMsg, slog.String("Error:", err.Error()))
		http.Error(w, errorMsg, http.StatusBadRequest)
	}

	//TODO Create communication with Mailer service.

	w.WriteHeader(http.StatusCreated)
}

// GetBooksByNameHandler handle HTTP GET requests to access specified books.
func GetBooksByNameHandler(w http.ResponseWriter, r *http.Request, name string, db *db.MongoDatabase) {
	if !validateName(name) {
		http.Error(w, "Invalid name parameter", http.StatusBadRequest)
	}

	ctx := r.Context()
	book, err := db.GetBooksByName(name, ctx)
	if err != nil {
		slog.Error(err.Error())
		http.Error(w, err.Error(), http.StatusBadRequest)
	}

	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(book)
}

// validateName checks if the book name is between 1 and 100 characters.
func validateName(name string) bool {
	return len(name) > 0 && len(name) < 100
}

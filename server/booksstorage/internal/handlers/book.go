package handlers

import (
	"booksstorage/internal/db"
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

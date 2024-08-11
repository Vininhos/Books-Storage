package handlers

import (
	"booksstorage-email/internal/models"
	"encoding/json"
	"net/http"
)

func SendEmailHandler(w http.ResponseWriter, r *http.Request) {
	var email models.Email
	json.NewDecoder(r.Body).Decode(&email)
	defer r.Body.Close()

}

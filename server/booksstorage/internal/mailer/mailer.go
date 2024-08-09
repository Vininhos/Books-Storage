package mailer

import (
	"booksstorage/internal/models"
	"bytes"
	"encoding/json"
	"log/slog"
	"net/http"
	"os"
)

func SendMailRequest(from, to, subject, body string) error {
	mail := models.Mailer{
		From:    os.Getenv("MAILER_FROM"),
		To:      os.Getenv("MAILER_TO"),
		Subject: "Thanks for adding a new book!",
		Body:    "New book was added! Thanks for contributing to BooksStorage.",
	}

	json, err := json.Marshal(mail)
	if err != nil {
		return err
	}

	payload := bytes.NewBuffer(json)

	resp, err := http.Post(os.Getenv("MAILER_URL"), "application/json", payload)
	if err != nil {
		return err
	}

	slog.Info("POST request was successfully sent to Mailer service.",
		"statusCode", resp.StatusCode)

	return nil
}

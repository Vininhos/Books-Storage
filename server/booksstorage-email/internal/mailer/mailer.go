package mailer

import (
	"booksstorage-email/internal/models"
	"fmt"
	"log/slog"
	"net/smtp"
)

func SendEmail(email models.Email) error {
	smtpServer := fmt.Sprintf("%s:%d", email.Domain, email.Port)
	auth := smtp.PlainAuth(email.Identity, email.Username, email.Password, email.Host)

	if err := smtp.SendMail(smtpServer, auth, email.From, email.To, []byte(email.Body)); err != nil {
		slog.Error("Error while sending email",
			"Error", err)
	}
	return nil
}

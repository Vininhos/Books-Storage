package logger

import (
	"log/slog"
	"os"

	"github.com/lmittmann/tint"
)

func GetLogger() *slog.Logger {
	logger := slog.New(tint.NewHandler(os.Stdout, nil))

	slog.SetDefault(logger)

	return logger
}

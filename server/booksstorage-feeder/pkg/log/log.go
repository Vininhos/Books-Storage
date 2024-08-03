package log

import (
	"log/slog"
	"os"

	"github.com/lmittmann/tint"
)

var logger *slog.Logger

func GetLogger() *slog.Logger {
	if logger != nil {
		return logger
	}

	logger = slog.New(tint.NewHandler(os.Stdout, nil))

	slog.SetDefault(logger)

	return logger
}

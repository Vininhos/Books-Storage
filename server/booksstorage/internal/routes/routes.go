package routes

import (
	"booksstorage/internal/db"
	"booksstorage/internal/handlers"
	"net/http"

	"github.com/go-chi/chi/v5"
	"github.com/go-chi/chi/v5/middleware"
	"github.com/go-chi/cors"
)

func Routes(db *db.MongoDatabase) http.Handler {
	r := chi.NewRouter()

	r.Use(middleware.RequestID)
	r.Use(middleware.RealIP)
	r.Use(middleware.Logger)
	r.Use(middleware.Recoverer)

	r.Use(cors.Handler(cors.Options{
		AllowedOrigins:   []string{"http://", "https://"},
		AllowedMethods:   []string{"GET", "POST", "PUT", "DELETE", "OPTIONS"},
		AllowedHeaders:   []string{"Accept", "Authorization", "Content-Type", "X-CSRF-Token"},
		ExposedHeaders:   []string{"Link"},
		AllowCredentials: false,
		MaxAge:           300,
	}))

	r.Use(middleware.Heartbeat("/ping"))

	r.Get("/", func(w http.ResponseWriter, r *http.Request) {
		w.Write([]byte("Welcome to BooksStorage!"))
	})

	r.Get("/health", func(w http.ResponseWriter, r *http.Request) {
		w.WriteHeader(http.StatusOK)
		w.Write([]byte("health is ok"))
	})

	r.Route("/api", func(r chi.Router) {
		r.Get("/book", func(w http.ResponseWriter, r *http.Request) {
			handlers.GetAllBooksHandler(w, r, db)
		})

		r.Post("/book", func(w http.ResponseWriter, r *http.Request) {
			handlers.InsertOneBookHandler(w, r, db)
		})

		r.Get("/book/name", func(w http.ResponseWriter, r *http.Request) {
			name := r.URL.Query().Get("name")
			handlers.GetBooksByNameHandler(w, r, name, db)
		})
	})

	return r
}

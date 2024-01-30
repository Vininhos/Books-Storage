package model

import "time"

type BooksStorageModel struct {
	Name            string
	Author          string
	PublicationYear int
	Price           float64
	Category        string
	Email           string
}

type PublicApiBook struct {
	Data struct {
		ID        int       `json:"id"`
		Year      int       `json:"Year"`
		Title     string    `json:"Title"`
		Handle    string    `json:"Handle"`
		Publisher string    `json:"Publisher"`
		ISBN      string    `json:"ISBN"`
		Pages     int       `json:"Pages"`
		Notes     []string  `json:"Notes"`
		CreatedAt time.Time `json:"created_at"`
		Villains  []struct {
			Name string `json:"name"`
			URL  string `json:"url"`
		} `json:"villains"`
	} `json:"data"`
}

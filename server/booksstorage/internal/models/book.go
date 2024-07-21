package models

type Book struct {
	Name     string  `bson:"name,omitempty"`
	Author   string  `bson:"author,omitempty"`
	PubYear  int     `bson:"publication_year,omitempty"`
	Price    float64 `bson:"price,omitempty"`
	Category string  `bson:"category,omitempty"`
}

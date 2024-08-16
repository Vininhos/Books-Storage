package models

type Email struct {
	Identity string
	Domain   string
	Host     string
	Port     int
	Username string
	Password string
	From     string
	To       []string
	Subject  string
	Body     string
}

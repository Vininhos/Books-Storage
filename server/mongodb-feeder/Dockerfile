FROM golang:1.21.6-alpine3.19 AS builder

WORKDIR /app

COPY . .

RUN CGO_ENABLED=0 GOOS=linux GOARCH=amd64 go build -o mongodbfeeder cmd/main/main.go

FROM scratch

COPY --from=builder /app/mongodbfeeder /mongodbfeeder

ENTRYPOINT [ "/mongodbfeeder" ]

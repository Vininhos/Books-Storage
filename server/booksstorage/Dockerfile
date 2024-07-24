FROM golang:1.22.4-alpine AS builder

WORKDIR /app

COPY . .

RUN CGO_ENABLED=0 GOOS=linux GOARCH=amd64 go build -o booksstorage cmd/main/main.go

FROM scratch

COPY --from=builder /app/booksstorage /booksstorage

ENTRYPOINT [ "/booksstorage" ]

FROM docker.io/golang:1.22.5-alpine AS builder

WORKDIR /app

COPY . .

RUN CGO_ENABLED=0 GOOS=linux GOARCH=amd64 go build -o booksstorage cmd/booksstorage/main.go

FROM gcr.io/distroless/base-debian12:nonroot

COPY --from=builder /app/booksstorage /booksstorage

ENTRYPOINT [ "/booksstorage" ]

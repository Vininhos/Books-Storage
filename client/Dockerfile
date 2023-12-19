FROM node:20 as build

WORKDIR /app

COPY ./ /app/

RUN npm install

RUN npm run build

FROM nginx:latest

COPY --from=build /app/dist/books-storage /usr/share/nginx/html

EXPOSE 80
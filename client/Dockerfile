FROM node:20 AS build

WORKDIR /app

COPY ./ /app/

RUN npm install

RUN npm run build --prod

FROM nginx:latest

COPY ./nginx/nginx.conf /etc/nginx/nginx.conf

RUN rm -rf /usr/share/nginx/html/*

COPY --from=build /app/dist/books-storage /usr/share/nginx/html

EXPOSE 80

ENTRYPOINT [ "nginx", "-g", "daemon off;" ]

import { Injectable } from "@angular/core";
import { BookReadDto } from "../models/book-read-dto";

import axios, { HttpStatusCode } from "axios";
import { BookCreateDto } from "../models/book-create-dto";
import Swal from "sweetalert2";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: "root",
})
export class BookService {
  bookList: BookReadDto[] = [];
  url = environment.apiUrl;

  constructor() {
    axios.defaults.baseURL = this.url;
  }

  insertBook(book: BookCreateDto) {
    axios
      .post(this.url + "/api/book", JSON.stringify(book), {
        headers: {
          "Content-Type": "application/json",
        },
      })
      .then((response) => {
        if (response.status == HttpStatusCode.Created) {
          Swal.fire({
            title: "Book added!",
            text: "Book successfully addded! Thanks for you contribution ❤️",
            icon: "success",
          });
        }
      }).catch(() => {
        Swal.fire({
          title: "Error!",
          text: "Something went wrong. Try contacting the developers.",
          icon: "error",
        });
      });
  }

  getAllBooks() {
    const response = axios.get(this.url + "/api/book");
    return response;
  }

  getBookById(id: string): BookReadDto | undefined {
    return this.bookList.find((book) => book.id === id);
  }
}

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

  async getAllBooks(): Promise<Array<BookReadDto>> {
    try {
      const response = await axios.get<Array<BookReadDto>>(this.url + "/api/book");

      if (response.status === HttpStatusCode.Ok) {
        return response.data;
      } else {
        return [];
      }
    } catch (error) {
      // Lidar com erros, se necessário
      console.error("Erro na chamada da API:", error);
      return [];
    }
  }

  getBookById(id: string): BookReadDto | undefined {
    return this.bookList.find((book) => book.id === id);
  }
}

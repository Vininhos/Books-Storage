import { Injectable } from "@angular/core";
import { BookReadDto } from "../models/book-read-dto";

import axios from "axios";
import { BookCreateDto } from "../models/book-create-dto";

@Injectable({
  providedIn: "root",
})
export class BookService {
  bookList: BookReadDto[] = [];
  url = "http://localhost:5240";

  constructor() {
    //axios.defaults.baseURL = this.url;
  }

  insertBook(book: BookCreateDto) {
    console.log(JSON.stringify(book));
    axios.post(this.url + "/api/book", JSON.stringify(book), {
      headers: {
        'Content-Type': 'application/json'
      }
    }).then((response) => {
      console.log("POST at " + this.url + "with response: " + response);
    });
  }

  async getAllBooks() {
    const response = axios.get(this.url+"/api/book");
    return response;
  }

  getBookById(id: string): BookReadDto | undefined {
    return this.bookList.find((book) => book.id === id);
  }
}

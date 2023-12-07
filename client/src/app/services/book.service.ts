import { Injectable } from "@angular/core";
import { Book } from "../models/book";
import axios from "axios";

@Injectable({
  providedIn: "root",
})
export class BookService {
  bookList: Book[] = [];
  url = "http://localhost:5240";

  constructor() {
    //axios.defaults.baseURL = this.url;
  }

  getAllBooks() {
    return axios.get("http://localhost:5240/api/book");
  }

  getBookById(id: string): Book | undefined {
    return this.bookList.find((book) => book.id === id);
  }
}

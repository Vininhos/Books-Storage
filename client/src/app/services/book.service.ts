import { Injectable } from '@angular/core';
import { Book } from '../models/book';
import axios from 'axios';

@Injectable({
  providedIn: 'root'
})

export class BookService {
  bookList: Book[] = [];
  url = "http://localhost:8079";

  constructor() {
    axios.defaults.baseURL = this.url;
   }

  async getAllBooks(): Promise<Book[] | undefined> {
    return axios.get("/api/book", {
      headers: {
        "Access-Control-Allow-Origin": "true"
      }
    });
  }

  getBookById(id: string): Book | undefined {
    return this.bookList.find(book => book.id === id);
  }
}

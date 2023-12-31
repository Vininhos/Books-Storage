import { Component, inject, OnInit } from '@angular/core';
import { BookReadDto } from '../models/book-read-dto';
import { BookService } from '../services/book.service';

@Component({
  selector: 'app-book-home',
  templateUrl: './book-home.component.html',
  styleUrls: ['./book-home.component.scss']
})
export class BookHomeComponent implements OnInit {
  bookService: BookService = inject(BookService);
  books: Array<BookReadDto> = [];

  ngOnInit() {
    this.getAllBooks();
  }

  async getAllBooks() {
    this.books = await this.bookService.getAllBooks();
  }
}

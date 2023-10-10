import { Component, Input, OnInit } from '@angular/core';
import { Book } from '../models/book';
import { BookService } from '../services/book.service';

@Component({
  selector: 'app-apicaller',
  templateUrl: './apicaller.component.html',
  styleUrls: ['./apicaller.component.scss']
})
export class ApicallerComponent {

  book = {} as Book;
  @Input() books: Book[] = [];

  constructor(private bookService: BookService) { }

  getBooks() {
    this.bookService.getBooks().subscribe((books: Book[]) => {
      this.books = books;
    });
  }
}
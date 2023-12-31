import { Component } from '@angular/core';
import { BookReadDto } from '../models/book-read-dto';

@Component({
  selector: 'app-book-home',
  templateUrl: './book-home.component.html',
  styleUrls: ['./book-home.component.scss']
})
export class BookHomeComponent {
  books: Array<BookReadDto>;

  constructor() {
    this.books = [
      new BookReadDto("1", 'Angular Development', "Wagner Less", 1999, 79.99, "Programming"),
      new BookReadDto("2", '.NET Development', "Yogo Wiz", 2023, 100.99, "Programming")
    ]
  }
}

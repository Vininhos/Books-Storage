import { Component, inject } from "@angular/core";
import { BookService } from "../services/book.service";

@Component({
  selector: "app-book-form",
  templateUrl: "./book-form.component.html",
  styleUrls: ["./book-form.component.scss"],
})
export class BookFormComponent {
  bookService: BookService = inject(BookService);

  constructor() {}
  
  getAllBooks() {
    this.bookService
      .getAllBooks()
      .then((response) => {
        console.log(response);
      })
      .catch((error) => {
        console.log(error);
      });
  }
}

import { Component } from "@angular/core";
import { BookService } from "../services/book.service";

@Component({
  selector: "app-book-form",
  templateUrl: "./book-form.component.html",
  styleUrls: ["./book-form.component.scss"],
})
export class BookFormComponent {
  constructor(private bookService: BookService) {}
  getAllBooks() {
    this.bookService.getAllBooks()
    .then(response => {
      console.log(response);
    })
    .catch(error => {
      console.log(error);
    })
  }
}

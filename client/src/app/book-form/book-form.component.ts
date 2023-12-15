import { Component, inject, Input } from "@angular/core";
import { BookService } from "../services/book.service";
import { FormControl, FormGroup, ReactiveFormsModule } from "@angular/forms";
import { BookCreateDto } from "../models/book-create-dto";

@Component({
  selector: "app-book-form",
  templateUrl: "./book-form.component.html",
  styleUrls: ["./book-form.component.scss"],
})
export class BookFormComponent {
  bookService: BookService = inject(BookService);

  bookForm = new FormGroup({
    name: new FormControl(""),
    author: new FormControl(""),
    publicationYear: new FormControl(""),
    price: new FormControl(""),
    category: new FormControl(""),
    email: new FormControl(""),
  }) as FormGroup & {value: BookCreateDto};

  newBook: BookCreateDto = new BookCreateDto();

  constructor() {}

  submitBook() {
    // if (this.bookForm.invalid) {
    //   return;
    // }

    this.newBook = this.bookForm.value;

    console.log(this.newBook);

    //console.log(this.bookService.getAllBooks());

    this.bookService.insertBook(this.newBook);
  }

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

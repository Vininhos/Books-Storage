export class BookCreateDto {
    public name: string;
    public author: string;
    public publicationYear: number;
    public price: number;
    public category: string;
    public email: string;

    constructor(
        name: string = '',
        author: string = '',
        yearOfPublication: number = 0o000,
        price: number = 0,
        category: string = '',
        email: string = ''
      ) {
        this.name = name;
        this.author = author;
        this.publicationYear = yearOfPublication;
        this.price = price;
        this.category = category;
        this.email = email;
      }
}

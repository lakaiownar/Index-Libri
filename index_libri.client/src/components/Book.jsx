
class Book {
    constructor(ibsn, googleid, title, author, pages, rating, bookCover, status) {
        this.isbn = ibsn;
        this.googleid = googleid;
        this.title = title;
        this.author = author;
        this.pages = pages;
        this.rating = rating;
        this.bookCover = bookCover;
        this.status = status;
    }
}

export default Book;
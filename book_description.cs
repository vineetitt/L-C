class Book {
    function getTitle() {
        return "A Great Book";
    }

    function getAuthor() {
        return "John Doe";
    }

    function turnPage() {
        // pointer to next page
    }

    function getCurrentPage() {
        return "current page content";
    }
}


class LibraryLocation {
    function getLocation(Book $book) {
        // returns the position in the library 
        // i.e shelf number & room number
    }
}


class BookPersistence {
    function save(Book $book) {
        $filename = '/documents/'. $book->getTitle(). ' - ' . $book->getAuthor();
        file_put_contents($filename, serialize($book));
    }
}

interface Printer {
    function printPage($page);
}

class PlainTextPrinter implements Printer {
    function printPage($page) {
        echo $page; 
    }
}

class HtmlPrinter implements Printer {
    function printPage($page) {
        echo '<div style="single-page">' . $page . '</div>'; 
    }
}

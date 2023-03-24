// import { HttpResponse } from "@angular/common/http"

export interface Page<T> {
    currentPage: number;
    pageSize: number;
    totalPages: number;
    totalItems: number;
    items: T[];
}

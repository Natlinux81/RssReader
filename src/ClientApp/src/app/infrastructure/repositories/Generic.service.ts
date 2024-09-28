import { Injectable } from '@angular/core';
import { IGenericRepository } from '../../domain/interfaces/IGenericRepository';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class GenericService<TEntity> implements IGenericRepository<TEntity> {

  private baseUrl = 'api/rssFeeds';

  constructor(private httpClient : HttpClient) { }

  getAllAsync(): Observable<TEntity[]> {
    return this.httpClient.get<TEntity[]>(this.baseUrl);
  }

  getByIdAsync(entity: TEntity): Observable<TEntity> {
    return this.httpClient.get<TEntity>(this.baseUrl + "/" + entity);
  }
  addAsync(entity: TEntity): Observable<TEntity> {
    return this.httpClient.post<TEntity>(this.baseUrl, entity);
  }
  update(entity: TEntity): TEntity {
    throw new Error('Method not implemented.');
  }
  delete(entity: TEntity):Observable <TEntity> {
    return this.httpClient.delete<TEntity>(this.baseUrl + "/" + entity);
  }
}

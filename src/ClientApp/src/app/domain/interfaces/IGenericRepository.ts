import { Observable } from "rxjs";

export interface IGenericRepository<TEntity> {
  getAllAsync(): Observable<TEntity[]>;
  getByIdAsync(entity: TEntity): Observable<TEntity>;
  addAsync(entity: TEntity): Observable<TEntity>;
  delete(entity: TEntity): void;
}

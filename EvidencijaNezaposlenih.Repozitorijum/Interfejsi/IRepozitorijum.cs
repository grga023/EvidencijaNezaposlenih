using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

public interface IRepozitorijum<T> where T : class
{
    Task<T?> Obrisi(object PK);
    Task<T?> DajSvePoPrimarnomKljucu(object PK);
    Task<T?> DajSvePoFilteru(object filter);
    Task<IEnumerable<T>> DajSve();
    T Dodaj(T obj);
    T? Izmeni(T obj);
    void Snimi();
}

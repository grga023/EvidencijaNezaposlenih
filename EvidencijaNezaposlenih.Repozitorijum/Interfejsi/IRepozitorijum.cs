using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

public interface IRepozitorijum<T> where T : class
{
    T DajSvePoID(object id);
    IEnumerable<T> DajSve();
    IEnumerable<T> Pronađi(Expression<Func<T, bool>> uslov);

    void Dodaj(T entitet);
    void DodajSve(IEnumerable<T> entiteti);

    void Ukloni(T entitet);
    void UkloniSve(IEnumerable<T> entiteti);
}

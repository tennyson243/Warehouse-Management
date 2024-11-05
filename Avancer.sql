Select Max(Circulation.Date) as Date, Colis.Designation  from Circulation 
inner join Colis on Colis.Id_Colis=Circulation.Colis where circulation.Type='Sortie' 
and Colis.Designation in(Select Colis.Designation from Magasinage inner join Colis on Colis.Id_Colis=Magasinage.Colis where Magasinage.Reste=0)
and Circulation.Reste in (Select Circulation.Reste from Circulation inner join Colis on Colis.Id_Colis=Circulation.Colis where Circulation.Reste=0)
and Colis.Designation in (Select Colis.Designation from Magasinage inner join Colis on colis.Id_Colis =Magasinage.Colis group by Colis.Designation having sum(Magasinage.Reste)=0)
group by Colis.Designation
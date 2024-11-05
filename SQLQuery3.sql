select circulation.Date, Colis.Designation, Circulation.Reste, case when count(type)=1 then 1  end as Total  from Circulation 
inner join Colis on Colis.Id_Colis=Circulation.Colis where circulation.Type='Sortie' 
and Colis.Designation in(Select Colis.Designation from Magasinage inner join Colis on Colis.Id_Colis=Magasinage.Colis where Magasinage.Reste=0
and Circulation.Reste in (Select Circulation.Reste from Circulation inner join Colis on Colis.Id_Colis=Circulation.Colis where Circulation.Reste=0
and Colis.Designation in (Select Colis.Designation from Circulation inner join Colis on Colis.Id_Colis=Circulation.Colis where Colis.Designation=Colis.Designation 
and Circulation.Date in (Select Max(Circulation.Date) from Circulation inner join Colis on Colis.Id_Colis=Circulation.Colis where Colis.Designation=Colis.Designation )
)))
group by Circulation.Date, Circulation.Type, colis.Designation, Circulation.Reste
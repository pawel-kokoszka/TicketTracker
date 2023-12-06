select 
	t.Id, 
	t.TypeId,
	tt.TypeName,
	t.ShortDescription,
	pc.Description,
	p.Name as "Project",
	et.Name as Environment


from Tickets t
	join TicketTypes tt on t.TypeId = tt.Id
	join ProjectConfigurations pc on t.ProjectConfigurationId = pc.Id
	join Projects p on pc.ProjectId = p.Id
	join Environments e on pc.EnvironmentId = e.Id
	join EnvironmentTypes et on e.EnvironmentTypeId = et.Id
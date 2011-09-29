print '<Event>'

print '<Tracks>'

select '<Track Name="' + RTRIM(REPLACE(Name, '&', '&amp;')) + '" />'
from Tracks

print '</Tracks>'

print '<Sessions>'

select 
	'<Session '
		+ 'Id="' + cast(s.Id as varchar(10)) + '" '
		+ 'Name="' + RTRIM(REPLACE(REPLACE(REPLACE(REPLACE(s.Name
			, CHAR(9), '')
			, '’', '''')
			, '–', '-')
			, '&', '&amp;')) + '" ' 
		+ 'Track="' + RTRIM(REPLACE(t.Name, '&', '&amp;')) + '" '
		+ 'StartTime="' + CONVERT(varchar(10), s.StartTime, 108) + '" '
		+ 'EndTime="' + CONVERT(varchar(10), s.EndTime, 108) + '" '
		+ 'Level="' + RTRIM(p.Level) + '" ' 
		+ 'Room="' + RTRIM(r.Name) + '" '
		+ 'Description="' + RTRIM(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(p.Description
			, '"', '''')
			, '“', '''')
			, '”', '''')
			, '’', '''')
			, '‘', '''')
			, '—', '-')
			, '•', '*')
			, '…', '...')
			, '&', '&amp;')) + '" '
		+ 'Speaker="' + RTRIM(isnull(speaker.Name,'')) + '" '
	 + '/>'
from Sessions s
left join Tracks t on s.TrackId = t.Id
left join EventPresentations ep on s.EventPresentation_Id = ep.Id
left join Presentations p on p.Id = ep.PresentationId
left join PresentationSpeakers ps on ps.PresentationsAsSpeaker_Id = ep.PresentationId
left join People speaker on speaker.Id = ps.Speakers_Id
left join Rooms r on r.Id = s.RoomId

print '</Sessions>'

print '<Speakers>'

select distinct
	'<Speaker '
		+ 'Name="' + RTRIM(speaker.Name) + '" '
		+ 'Title="' + RTRIM(REPLACE(speaker.Title, '&', '&amp;')) + '" '
		+ 'Bio="' + RTRIM(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(speaker.Bio
			, ' ', ' ')
			, '&', '&amp;')
			, '"', '''')
			, '`', '''')
			, '…', '...')
			, '’', '''')
			, '‘', '''')) + '" '
		+ 'Blog="' + RTRIM(speaker.Blog) + '" '
		+ 'Twitter="' + RTRIM(speaker.Twitter) + '" '
	 + '/>'
from Sessions s
join Tracks t on s.TrackId = t.Id
join EventPresentations ep on s.EventPresentation_Id = ep.Id
join Presentations p on p.Id = ep.PresentationId
join PresentationSpeakers ps on ps.PresentationsAsSpeaker_Id = ep.PresentationId
join People speaker on speaker.Id = ps.Speakers_Id

print '</Speakers>'

print '</Event>'
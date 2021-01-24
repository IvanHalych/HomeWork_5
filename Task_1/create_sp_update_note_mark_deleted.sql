CREATE or replace FUNCTION update_note_mark_deleted(_id uuid) returns
    TABLE(id uuid  ,header varchar(128),body varchar(1024),
    is_deleted boolean , modified_at timestamptz,
    user_id int)
AS
    $$
    begin
        UPDATE notes SET  is_deleted  =true, modified_at = current_timestamp
        WHERE notes.id = $1;
        return QUERY
            SELECT notes.id,notes.header,notes.body,
                   notes.is_deleted,notes.modified_at,
                   notes.user_id
            from notes
            where notes.id = $1;
    end
    $$
LANGUAGE plpgsql;